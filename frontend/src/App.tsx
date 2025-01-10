import { useEffect, useState } from 'react'
import './App.css'
import ClickableTrackName from './components/ClickableTrackName'

import api from './api/api'
import { DashboardModel } from './api/client';
import CurrentLeaderboard from './components/CurrentLeaderBoard';

function App() {

  const [dashboard, setDashboard] = useState<DashboardModel>();

  useEffect(() => {
    api.getDashboard().then(r => {
      if (r.error) {
        console.error(r.error);
      } else if (r.data) {
        console.log(r.data);
        setDashboard(r.data);
      }
    })
  }, []);

  return (
    <>
      <main className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 text-white">
        <div className="max-w-7xl mx-auto px-4 py-12 sm:px-6 lg:px-8">
          <header className="mb-12 text-center">
            <h1 className="text-4xl md:text-5xl font-bold bg-gradient-to-r from-emerald-400 to-cyan-400 bg-clip-text text-transparent">
              UA Velocidrone Battle
            </h1>
          </header>

          <div className="grid lg:grid-cols-2 gap-8">
            {/* Current Competition */}
            <div className="bg-gray-800 rounded-lg shadow-lg overflow-hidden">
              <div className="px-6 py-8 border-b border-gray-700">
                <div className="space-y-2">
                  <h3 className="text-sm uppercase tracking-wider text-emerald-400 font-medium">
                    Трек сьогодні:
                  </h3>
                  <ClickableTrackName trackName={dashboard?.competition?.trackName} />
                </div>
              </div>
              <CurrentLeaderboard trackResults={dashboard?.results} />
            </div>

            {/* Yesterday's Results */}
            <div className="bg-gray-800 rounded-lg shadow-lg overflow-hidden">
              <div className="px-6 py-8 border-b border-gray-700">
                <h3 className="text-sm uppercase tracking-wider text-emerald-400 font-medium">
                  Результати вчорашнього дня:
                </h3>
              </div>
              <h2 className="text-center">Comming soon...</h2>
            </div>
          </div>
        </div>
      </main>
    </>
  )
}

export default App
