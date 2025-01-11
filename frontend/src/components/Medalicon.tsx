interface MedalIconProps {
    color: string;
    className?: string;
}

export function MedalIcon({ color, className = "" }: MedalIconProps) {
    return (
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill={color}
        className={`w-6 h-6 ${className}`}
      >
        <path d="M12 7.5l1.5 4h4l-3 3 1 4-3.5-2-3.5 2 1-4-3-3h4z" />
        <circle cx="12" cy="12" r="10" fill="none" stroke={color} strokeWidth="2" />
      </svg>
    );
  }