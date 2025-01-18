import { Calendar, BarChart, Trophy, Users } from "lucide-react";
import SideMenuItem from "./SideMenuItem";

const SideMenu = () => {
    return (
        <aside className="lg:w-64 bg-slate-800/50 backdrop-blur-sm border border-slate-700 rounded-lg overflow-hidden">
            <nav className="p-4">
                <ul className="space-y-2">
                    <li>
                        <SideMenuItem to="heatmap" icon={Calendar} label="Heat map" />
                    </li>

                    <li>
                        <SideMenuItem to="pilots" icon={Users} label="Pilot Stats" />
                    </li>

                    <li>
                        <SideMenuItem to="tracks" icon={BarChart} label="Track Stats" />
                    </li>

                    <li className="py-2">
                        <div className="border-t border-slate-700"></div>
                    </li>

                    <li>
                        <SideMenuItem to="leaderboard" icon={Trophy} label="Leaderboard" />
                    </li>
                </ul>
            </nav>
        </aside>
    );
};

export default SideMenu;
