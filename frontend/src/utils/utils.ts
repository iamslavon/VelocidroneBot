export const convertMsToSec = (milliseconds: number): string => {
    return (milliseconds / 1000).toFixed(3) + 's';
};
