export async function AuthorizeUserData(login: string, password: string): Promise<boolean> {
    return fetch("/api/authorize", { method: 'POST', body: JSON.stringify({ login, password }), headers: { 'Content-Type': 'application/json'} })
    .then(_ => true)
    .catch(_ => false);
}
