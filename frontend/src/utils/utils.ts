export const convertMsToSec = (milliseconds: number | undefined): string => {
    if (milliseconds === undefined) {
        return '-';
    }
    return (milliseconds / 1000).toFixed(2) + 's';
};
