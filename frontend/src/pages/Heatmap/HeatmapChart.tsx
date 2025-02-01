import { PilotResult } from '@/api/client';
import { CalendarDatum, ResponsiveCalendarCanvas } from '@nivo/calendar'
import { Theme } from '@nivo/core';

interface HeatmapChartProps {
    data: PilotResult[]
}

const theme: Theme = {

};

const HeatmapChart = ({ data }: HeatmapChartProps) => {
    const d = data.map(i => ({
        day: new Date(i.date!).toISOString().split('T')[0],
        value: i.points
    } as CalendarDatum));

    return <>
        <ResponsiveCalendarCanvas
            data={d}
            from={d[0].day}
            to={d[d.length - 1].day}
            emptyColor="#eeeeee"
            colors={['#A8E6CF', '#77DD77']}
            margin={{ top: 40, right: 40, bottom: 40, left: 40 }}
            yearSpacing={40}
            monthBorderColor="#ffffff"
            dayBorderWidth={2}
            dayBorderColor="#ffffff"
            theme={
                theme
            }
            legends={[
                {
                    anchor: 'bottom-right',
                    direction: 'row',
                    translateY: 36,
                    itemCount: 4,
                    itemWidth: 42,
                    itemHeight: 36,
                    itemsSpacing: 14,
                    itemDirection: 'right-to-left'
                }
            ]}
        />

    </>
}

export default HeatmapChart;