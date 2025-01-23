import { useAppDispatch, useAppSelector } from '@/lib/hooks';
import { fetchPilots, selectPilots, selectPilotsState } from '@/lib/features/pilots/pilotsSlice';
import { useEffect, Suspense, lazy } from 'react';
import ComboBox from '@/components/ComboBox';
import { fetchHeatmap, choosePilot, selectHeatmapState, selectCurrentHeatmap, selectCurrentPilot } from '@/lib/features/heatmap/heatmapSlice';

const HeatmapChart = lazy(() => import('./HeatmapChart'))

const pilotKey = (pilot: string) => pilot;
const pilotLabel = (pilot: string) => pilot;

const PageHeatmap = () => {

    const dispatch = useAppDispatch();
    const pilotsState = useAppSelector(selectPilotsState);
    const pilots = useAppSelector(selectPilots);
    const heatMapState = useAppSelector(selectHeatmapState);
    const heatMap = useAppSelector(selectCurrentHeatmap);
    const currentPilot = useAppSelector(selectCurrentPilot);


    useEffect(() => {
        if (pilotsState == 'Idle' || pilotsState == 'Error') {
            dispatch(fetchPilots());
        }
    }, [pilotsState, dispatch]);

    const selectPilot = (pilot: string) => {
        dispatch(choosePilot(pilot));
        dispatch(fetchHeatmap(pilot));
    }

    if (pilotsState == 'Idle') return <></>;

    if (pilotsState == 'Loading') return <h2 className='text-center text-2xl text-green-500'>🚁 Loading</h2>

    if (pilotsState == 'Error') return <h2>Error</h2>

    return <>
        <ComboBox defaultCaption='Select a pilot'
            items={pilots}
            getKey={pilotKey}
            getLabel={pilotLabel}
            onSelect={selectPilot}
            value={currentPilot!}></ComboBox>

        <div className='py-6'>

            {heatMapState == 'Loading' && <>
                <div className='flex space-x-2 justify-center items-center bg-white h-screen dark:invert rounded-lg' style={{ height: '300px' }}>
                    <span className='sr-only'>Loading...</span>
                    <div className='h-8 w-8 bg-black rounded-full animate-bounce [animation-delay:-0.3s]'></div>
                    <div className='h-8 w-8 bg-black rounded-full animate-bounce [animation-delay:-0.15s]'></div>
                    <div className='h-8 w-8 bg-black rounded-full animate-bounce'></div>
                </div>
            </>
            }

            {heatMapState == 'Loaded' && <>
                <div className='bg-slate-200 rounded-lg' style={{ height: '600px' }}>
                    <Suspense fallback={<div>Loading...</div>}>
                        <HeatmapChart data={heatMap} />
                    </Suspense>
                </div>
            </>
            }
        </div>

    </>
}

export default PageHeatmap;