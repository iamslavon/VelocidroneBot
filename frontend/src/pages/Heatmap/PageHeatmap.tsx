import { useAppDispatch, useAppSelector } from '@/lib/hooks';
import { fetch, selectPilots, selectState } from '@/lib/features/pilots/pilotsSlice'
import { useEffect } from 'react';
import { ComboboxDemo } from '@/components/ComboBox';


const PageHeatmap: React.FC = () => {

    const dispatch = useAppDispatch();
    const pilotsState = useAppSelector(selectState);
    const pilots = useAppSelector(selectPilots);

    useEffect(() => {
        if (pilotsState == 'Idle' || pilotsState == 'Error') {
            dispatch(fetch());
        }
    }, [pilotsState]);

    if (pilotsState == 'Idle') return <></>;

    if (pilotsState == 'Loading') return <h2 className='text-center text-2xl text-green-500'>🚁 Loading</h2>

    if (pilotsState == 'Error') return <h2>Error</h2>

    return <>
        <h2 className="text-center text-2xl text-green-500">🤞Heatmap is in Progress</h2>

        <ComboboxDemo></ComboboxDemo>

        <h3 className='p-4 text-green-200 text-xl'>Pilots:</h3>

        <ul>
            {pilots.map(p => <li key={p}><span className='text-gray-300'>{p}</span></li>)}
        </ul>
    </>
}

export default PageHeatmap;