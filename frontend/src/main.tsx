import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom'
import LayoutMain from './pages/LayoutMain.tsx'
import PageRules from './pages/PageRules.tsx'
import PageStatistics from './pages/Statistics/PageStatistics.tsx'
import PageDashboard from './pages/PageDashboard.tsx'
import { Provider } from 'react-redux'
import { store } from './lib/store'
import PageHeatmap from './pages/Heatmap/PageHeatmap.tsx'
import PageTracks from './pages/Tracks/PageTracks.tsx'
import PageLeaderBoard from './pages/LeaderBoard/PageLeaderBoard.tsx'
import PagePilots from './pages/Pilots/PagePilots.tsx'


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path='/'>
            <Route element={<LayoutMain />}>
              <Route index element={<PageDashboard />} />
              <Route path='rules' element={<PageRules />} />
              <Route path='statistics' element={<PageStatistics />} >
                <Route index element={<Navigate to="heatmap" replace />} />
                <Route path="heatmap" element={<PageHeatmap />} />
                <Route path="leaderboard" element={<PageLeaderBoard />} />
                <Route path="tracks" element={<PageTracks />} />
                <Route path="pilots" element={<PagePilots />} />
              </Route>
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </Provider>
  </StrictMode>
)
