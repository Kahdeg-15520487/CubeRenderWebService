# CubeRender — supply-chain demo app

A deliberately tiny, slightly silly app built as the *demo vehicle* for a software
supply-chain pipeline (sign → SBOM → attestation → Harbor → verified deploy in k3s).
The app itself is not the point — the **CI/CD** is:

- **`backend/`** — a modern **.NET 8** minimal API (`net8.0`, top-level statements).
  One endpoint, `GET /api/render/{x}/{y}/{z}`, returns an ASCII wireframe cube.
  - `GET /api/health` → liveness/readiness probe.
- **`frontend/`** — a modern **React 18 + Vite + TypeScript** SPA that calls the API
  and draws the cube. Dev proxies `/api` to `http://localhost:8080`; in the container
  nginx proxies `/api` to the `cuberender-api` Service.

Two deliberately different stacks (`.NET/nuget` vs `Node/npm`) so that **syft generates
two genuinely different SBOMs** and each image gets its **own cosign signature** —
demonstrating the signing/SBOM flow across two ecosystems from one pipeline.

## Two images

| Image | Stack | Base | Dependencies (SBOM) |
|---|---|---|---|
| `harbor.minhnguyenle.net/supply/cuberender-api` | .NET 8 | `mcr.microsoft.com/dotnet/aspnet:8.0` | nuget / .NET |
| `harbor.minhnguyenle.net/supply/cuberender-web` | Node 22 → nginx | `nginx:1.27-alpine` | npm packages |

Each image is tagged `:sha-<git sha>` so the image maps 1:1 to the (signed) commit.

## Local dev

```bash
# backend   (API on :8080)
cd backend && dotnet run

# frontend  (dev server on :5173, proxies /api -> :8080)
cd frontend && npm install && npm run dev
```

## Local build (both stacks)

```bash
cd backend  && dotnet publish -c Release -o out
cd frontend && npm ci && npm run build
```

## Supply-chain pipeline (`.github/workflows/supply-chain.yaml`)

On push it:

1. **Verifies the commit is signed** (`REQUIRE_SIGNED_COMMIT=true` → fails if not).
2. Builds + pushes **both** images to Harbor.
3. `syft` → SPDX **SBOM** for each image.
4. `cosign sign` → signature for each image.
5. `cosign attest --type spdxjson` → SBOM as an in-toto **attestation**.
6. `cosign attest --type slsaprovenance1` → **SLSA** provenance (binds repo + commit SHA).

The pipeline authenticates to Harbor with a robot account and signs with a cosign
keypair held in GitHub Actions secrets (`COSIGN_PRIVATE_KEY`, `COSIGN_PASSWORD`,
`HARBOR_PUSH_PASS`).

## Verification in k3s

The `sigstore/policy-controller` admission operator (in the k3s lab, see
`k3s_lab/supply-chain/PROPOSAL.md`) requires every image deployed to the `supply-prod`
namespace to carry a valid signature **and** SBOM + SLSA attestations. An unsigned or
unattested image is rejected at pod-creation time.
