import { MedalIcon } from "./Medalicon";

interface LeaderBoardMedalProps {
    place: number;
}
const LeaderBoardMedal: React.FC<LeaderBoardMedalProps> = ({ place }) => {
    return <span className="font-medium text-gray-500 mr-4 w-6 text-right flex justify-end items-center text-lg">
        {place === 0 && <MedalIcon color="#10B981" />} {/* Emerald-500 */}
        {place === 1 && <MedalIcon color="#94A3B8" />} {/* Gray-400 */}
        {place === 2 && <MedalIcon color="#475569" />} {/* Gray-600 */}
        {place > 2 && `${place + 1}`}
    </span>
};
export default LeaderBoardMedal;