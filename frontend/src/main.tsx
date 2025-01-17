import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LayoutMain from './pages/LayoutMain.tsx'
import PageRules from './pages/PageRules.tsx'
import PageStatistics from './pages/PageStatistics.tsx'
import PageDashboard from './pages/PageDashboard.tsx'
import { Provider } from 'react-redux'
import { store } from './lib/store'



createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
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
    </Provider>
  </StrictMode>
)
