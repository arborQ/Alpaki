import { CurrentUser, AuthorizeMode } from "Contexts/AuthorizeContext";

const users: CurrentUser[] = [
    { id: 1, login: 'admin1', mode: AuthorizeMode.Full },
    { id: 2, login: 'ola', mode: AuthorizeMode.Shop },
    { id: 3, login: 'kasia', mode: AuthorizeMode.Baby },
];

export async function AuthorizeUserData(login: string, password: string): Promise<CurrentUser> {
    const pro = await fetch("/api/ValidateUser", { method: 'POST', body: JSON.stringify({ login, password }), headers: { 'Content-Type': 'application/json'} })
    .then(_ => true)
    .catch(_ => false);
    return new Promise<CurrentUser>(
        resolve => {
            const [user] = users.filter(u => u.login === login && password === 'test');
            resolve(user);
        });
}
