import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LayoutMain from './pages/LayoutMain.tsx'
import PageRules from './pages/PageRules.tsx'
import PageStatistics from './pages/PageStatistics.tsx'
import PageDashboard from './pages/PageDashboard.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path='/'>
          <Route element={<LayoutMain />}>
            <Route index element={<PageDashboard />} />
            <Route path='rules' element={<PageRules />} />
            <Route path='statistics' element={<PageStatistics />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
