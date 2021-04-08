import { CurrentUser, AuthorizeMode } from "Contexts/AuthorizeContext";

const users: CurrentUser[] = [
    { id: 1, login: 'admin', mode: AuthorizeMode.Full },
    { id: 2, login: 'ola', mode: AuthorizeMode.Shop },
    { id: 3, login: 'kasia', mode: AuthorizeMode.Baby },
];

export async function AuthorizeUserData(login: string, password: string): Promise<CurrentUser> {
    // return fetch("/api/authorize", { method: 'POST', body: JSON.stringify({ login, password }), headers: { 'Content-Type': 'application/json'} })
    // .then(_ => true)
    // .catch(_ => false);

    return new Promise<CurrentUser>(
        resolve => {
            const [user] = users.filter(u => u.login === login && password === 'test');
            resolve(user);
        });
}
