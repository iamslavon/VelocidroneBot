import { SeasonResultModel } from "../api/client";
import LeaderBoardMedal from "./LeaderBoardMedal";

interface LeaderBoardProps {
    leaderBoard: SeasonResultModel[];
}

const LeaderBoard: React.FC<LeaderBoardProps> = ({ leaderBoard }) => {
    if (!leaderBoard) return <>Завантажуємо</>;

    if (!leaderBoard.length) return <>Немає даних</>;

    return <>

        <div className="overflow-hidden">
            <ul className="divide-y divide-gray-700">
                {leaderBoard.map((res, index) => (
                    <li key={res.playerName} className="px-6 py-4 hover:bg-slate-700/30 transition-colors duration-150">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center">
                                <span className="font-bold text-slate-400 mr-4 w-8 text-right text-2xl tabular-nums">
                                    <LeaderBoardMedal place={index} />
                                </span>
                                <p className="text-sm font-medium text-slate-200">
                                    {res.playerName}
                                </p>
                            </div>
                            <div className="text-lg text-slate-300 font-semibold tabular-nums">{res.points}</div>
                        </div>
                    </li>
                ))}
            </ul>
        </div>

    </>
}

export default LeaderBoard;