import { SeasonResultModel } from "../api/client";

interface LeaderBoardProps {
    leaderBoard: SeasonResultModel[];
}

const LeaderBoard: React.FC<LeaderBoardProps> = ({ leaderBoard }) => {
    if (!leaderBoard) return <>Завантажуємо...</>;

    if (!leaderBoard.length) return <><div className="text-green-200 text-center">Немає даних</div></>;

    return <>

        <div className="overflow-hidden">
            <div className="px-6 py-4 border-b border-slate-700/50 grid grid-cols-2 gap-4">
                <div className="text-sm font-medium text-emerald-400">Пілот</div>
                <div className="text-sm font-medium text-emerald-400 text-right">Очки</div>
            </div>
            <ul className="divide-y divide-slate-700/50">
                {leaderBoard.map((res, index) => (
                    <li key={res.playerName} className="px-6 py-4 hover:bg-slate-700/30 transition-colors duration-150">
                        <div className="flex items-center justify-between">
                            <div className="flex items-center">
                                <span className="font-bold text-slate-400 mr-4 w-8 text-right text-2xl tabular-nums">
                                    {index + 1}
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