import Layout from '../../components/layout';
import { postData, PostModel } from '../../sources/post-source';
import Link from 'next/link'


export default ({ allPostsData }: { allPostsData: PostModel[] }) => {
    return (
        <Layout>
            <div>Post count {allPostsData.length}</div>
            {
                allPostsData.map(p => (
                    <div key={p.id}>
                        <Link href={`/posts/${p.id}`}>
                            <a>{p.name}</a>
                        </Link>
                    </div>
                ))
            }
        </Layout>
    )
}

export async function getStaticProps() {
    return {
        props: {
            allPostsData : postData
        }
    }
}