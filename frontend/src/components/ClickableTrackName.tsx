import { useState } from 'react'
import { Check, Copy } from 'lucide-react'

interface ClickableTrackNameProps {
    trackName: string
}

const ClickableTrackName: React.FC<ClickableTrackNameProps> = ({ trackName }: ClickableTrackNameProps) => {
    const [copied, setCopied] = useState(false)

    const copyToClipboard = async () => {
        try {
            await navigator.clipboard.writeText(trackName);
            setCopied(true);
            setTimeout(() => setCopied(false), 2000);
        } catch (err) {
            console.error('Failed to copy text: ', err);
        }
    }

    return (
        <button
            onClick={copyToClipboard}
            className="text-2xl font-semibold text-white flex items-center gap-2 hover:text-emerald-400 transition-colors duration-200 group"
        >
            {trackName}
            {copied ? (
                <Check className="h-5 w-5 text-emerald-400" />
            ) : (
                <Copy className="h-5 w-5 text-gray-500 group-hover:text-emerald-400 transition-colors" />
            )}
        </button>
    )
}

export default ClickableTrackName;