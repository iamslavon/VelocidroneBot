


function App() {


  return (
    <>

            </h1>
            <nav className="flex justify-center space-x-4">

            </nav>
          </header>

          <div className="grid lg:grid-cols-2 gap-8">
            {/* Current Competition */}
            <div className="bg-slate-800/50 backdrop-blur-sm rounded-2xl border border-slate-700 overflow-hidden">
              <div className="px-6 py-8 border-b border-slate-700/50">
                <div className="flex flex-col space-y-1">
                  <h3 className="text-sm uppercase tracking-wider text-emerald-400 font-medium mb-2">
                    Трек сьогодні:
                  </h3>
                  <ClickableTrackName mapName={dashboard.competition.mapName!} trackName={dashboard.competition.trackName!} />
                </div>
              </div>
              <CurrentLeaderboard trackResults={dashboard.results!} />
            </div>

            {/* Tournament Leaderboard */}
            <div className="bg-slate-800/50 backdrop-blur-sm rounded-2xl border border-slate-700 overflow-hidden">
              <div className="px-6 py-8 border-b border-slate-700/50">
                <h3 className="text-sm uppercase tracking-wider text-emerald-400 font-medium">
                  ПОТОЧНА ТУРНІРНА ТАБЛИЦЯ
                </h3>
              </div>
              <LeaderBoard leaderBoard={dashboard.leaderboard}></LeaderBoard>
            </div>
          </div>
        </div>
      </main>
    </>
  )
}

export default App
