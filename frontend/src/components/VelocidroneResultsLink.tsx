import { Link } from "react-router-dom";

interface IVelocidroneResultLink {
    MapId: number,
    TrackId: number
}

interface IVelocidroneResultLinkProps {
    trackInfo: IVelocidroneResultLink
}

const VelocdroneResultLink: React.FC<IVelocidroneResultLinkProps> = ({ trackInfo }) => {
    if (!trackInfo) return <></>;
    return <>
        <Link
            className="text-sm text-slate-400 hover:text-emerald-400 transition-colors mt-2 inline-flex items-center gap-1"
            to={`https://www.velocidrone.com/leaderboard/${trackInfo.MapId}/${trackInfo.TrackId}/All`}
            target="_blank">
            Переглянути таблицю лідерів на Velocidrone
        </Link>
    </>
}

export default VelocdroneResultLink;