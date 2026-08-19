import { useCallback, useEffect, useState } from 'react'

type RenderResult = { lines: string[]; width: number; height: number }
type Health = { status: string; ts: string }

const DEFAULT = { x: 8, y: 6, z: 5 }

const FALLBACK: RenderResult = {
  lines: [
    '   ###########',
    '  #.........#',
    ' #..........#',
    '#...........#',
    '############.',
  ],
  width: 14,
  height: 5,
}

export default function App() {
  const [x, setX] = useState(DEFAULT.x)
  const [y, setY] = useState(DEFAULT.y)
  const [z, setZ] = useState(DEFAULT.z)
  const [render, setRender] = useState<RenderResult | null>(null)
  const [health, setHealth] = useState<Health | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)

  const fetchHealth = useCallback(async () => {
    try {
      const res = await fetch('/api/health')
      if (res.ok) setHealth(await res.json())
    } catch {
      setHealth(null)
    }
  }, [])

  const fetchRender = useCallback(async (rx: number, ry: number, rz: number) => {
    setLoading(true)
    setError(null)
    try {
      const res = await fetch(`/api/render/${rx}/${ry}/${rz}`)
      if (!res.ok) throw new Error(`HTTP ${res.status}`)
      setRender(await res.json())
    } catch (e) {
      setError(e instanceof Error ? e.message : 'render failed')
      setRender(FALLBACK)
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    void fetchHealth()
    void fetchRender(DEFAULT.x, DEFAULT.y, DEFAULT.z)
  }, [fetchHealth, fetchRender])

  return (
    <main className="app">
      <header>
        <h1>🧊 CubeRender</h1>
        <p>modern .NET 8 API + React frontend — supply-chain signed</p>
        <span className={`dot ${health ? 'ok' : 'down'}`} title={health ? `api ${health.status}` : 'api down'}>
          api {health ? 'up' : 'down'}
        </span>
      </header>

      <section className="controls">
        <label>
          x <input type="number" value={x} onChange={(e) => setX(+e.target.value)} min={1} max={40} />
        </label>
        <label>
          y <input type="number" value={y} onChange={(e) => setY(+e.target.value)} min={1} max={40} />
        </label>
        <label>
          z <input type="number" value={z} onChange={(e) => setZ(+e.target.value)} min={1} max={40} />
        </label>
        <button onClick={() => void fetchRender(x, y, z)} disabled={loading}>
          {loading ? 'rendering…' : 'Render cube'}
        </button>
      </section>

      {error && <p className="error">{error}</p>}

      <pre className="cube">{render ? render.lines.join('\n') : ''}</pre>

      <footer>
        <code>GET /api/render/{x}/{y}/{z}</code>
      </footer>
    </main>
  )
}
