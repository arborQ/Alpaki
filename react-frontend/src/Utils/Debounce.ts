export function debounce<T extends Function>(cb: T, wait = 20) {
    let h: number | undefined;
    let callable = (...args: any) => {
        clearTimeout(h);
        h = setTimeout(() => cb(...args), wait) as any;
    };

    return callable as any as T;
}

export function debounceAsync<T extends Function>(cb: T, wait = 20) {
    let h: number | undefined;
    let callable = (...args: any) => {
        clearTimeout(h);
        return new Promise((resolve) => {
            h = setTimeout(() => resolve(cb(...args)), wait) as any;
        });
    };

    return callable as any as T;
}