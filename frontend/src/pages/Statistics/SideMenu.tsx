import { Link } from "react-router-dom";
import { Calendar, BarChart, Trophy, ChevronRight, Users, Check, ChevronsUpDown } from 'lucide-react';

const SideMenu = () => {
    return <>
        <aside className="lg:w-64 bg-slate-800/50 backdrop-blur-sm border border-slate-700 rounded-lg overflow-hidden">
            <nav className="p-4">
                <ul className="space-y-2">
                    <li>
                        <Link to="heatmap" className="flex items-center justify-between w-full text-left text-slate-200 hover:text-emerald-400 transition-colors py-2 px-3 rounded"
                        >
                            <span className="flex items-center">
                                <Calendar className="h-5 w-5 mr-2" />
                                <span>Heat map</span>
                            </span>
                            <ChevronRight className="h-4 w-4" />
                        </Link>
                    </li>
                    <li>
                        <Link to="pilots"
                            className="flex items-center justify-between w-full text-left text-slate-200 hover:text-emerald-400 transition-colors py-2 px-3 rounded"
                        >
                            <span className="flex items-center">
                                <Users className="h-5 w-5 mr-2" />
                                <span>Pilot Stats</span>
                            </span>
                            <ChevronRight className="h-4 w-4" />
                        </Link>
                    </li>
                    <li>
                        <Link to="tracks"
                            className="flex items-center justify-between w-full text-left text-slate-200 hover:text-emerald-400 transition-colors py-2 px-3 rounded"
                        >
                            <span className="flex items-center">
                                <BarChart className="h-5 w-5 mr-2" />
                                <span>Track Stats</span>
                            </span>
                            <ChevronRight className="h-4 w-4" />
                        </Link>
                    </li>
                    <li className="py-2">
                        <div className="border-t border-slate-700"></div>
                    </li>
                    <li>
                        <Link to="leaderboard"
                            className="flex items-center justify-between w-full text-left text-slate-200 hover:text-emerald-400 transition-colors py-2 px-3 rounded"
                        >
                            <span className="flex items-center">
                                <Trophy className="h-5 w-5 mr-2" />
                                <span>Leaderboard</span>
                            </span>
                            <ChevronRight className="h-4 w-4" />
                        </Link>
                    </li>
                </ul>
            </nav>
        </aside>
    </>
}

export default SideMenu;