import { TrackTimeModel } from "../api/client";
import { convertMsToSec } from "../utils/utils";

interface CurrentLeaderboardProps {
    trackResults: TrackTimeModel[];
}

const CurrentLeaderboard: React.FC<CurrentLeaderboardProps> = ({ trackResults }: CurrentLeaderboardProps) => {

    if (!trackResults || !trackResults.length) return <></>;

    return (
        <div className="overflow-hidden">
            <ul className="divide-y divide-gray-700">
                {trackResults.map((pilot, index) => (
                    <li key={pilot.playerName} className="px-6 py-4 hover:bg-gray-700/50 transition-colors duration-150">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center">
                                <span className="font-medium text-gray-500 mr-4 w-6 text-right text-lg tabular-nums">
                                    {index + 1}
                                </span>
                                <p className="text-sm font-medium text-gray-200">
                                    {pilot.playerName}
                                </p>
                            </div>
                            <div className="text-sm text-gray-400 font-medium tabular-nums">{convertMsToSec(pilot.time)}</div>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default CurrentLeaderboard;