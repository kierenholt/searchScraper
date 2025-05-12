
export class Helpers {


    static get<T>(url: string): Promise<T> {
        return fetch(url)
            .then((req: Response) => req.json())
    }

    static post(url: string, data: any): Promise<boolean> {
        return fetch(url, {
            method: "post", body: JSON.stringify(data),
            headers: { 'Content-Type': 'application/json' },
        })
            .then((req: Response) => req.json())
    }

    static put<T>(url: string, data: any): Promise<T> {
        return fetch(url, {
            method: "put", body: JSON.stringify(data),
            headers: { 'Content-Type': 'application/json' },
        })
            .then((req: Response) => req.json())
    }

    static delete(url: string): Promise<number> {
        return fetch(url, {
            method: "delete",
            headers: { 'Content-Type': 'application/json' },
        })
            .then((req: Response) => req.json())
    }

}