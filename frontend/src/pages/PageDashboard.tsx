import { useEffect, useState } from 'react'
import ClickableTrackName from './../components/ClickableTrackName'

import api from './../api/api'
import { DashboardModel } from './../api/client';
import CurrentLeaderboard from './../components/CurrentLeaderBoard';
import LeaderBoard from './../components/LeaderBoard';

const PageDashboard: React.FC = () => {


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

    //TODO: Add proper loading state and indicator
    if (!dashboard) {
        return <></>
    }

    return <>
        <div className="grid lg:grid-cols-2 gap-8">
            {/* Current Competition */}
            <div className="bg-gray-800 rounded-lg shadow-lg overflow-hidden">
                <div className="px-6 py-8 border-b border-gray-700">
                    <div className="space-y-2">
                        <h3 className="text-sm uppercase tracking-wider text-emerald-400 font-medium">
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
    </>

}

export default PageDashboard;