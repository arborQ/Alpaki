export interface PostModel {
    id: string;
    name: string;
    date: number;
}

let allPostsData: PostModel[] = [
];

for (let i = 0; i < 10000; i++) {
    allPostsData = [...allPostsData, { id: i.toString(), name: `Post number: ${i}!`, date: new Date().getDate() }]
}

export const postData = allPostsData;