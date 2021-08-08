import Layout from '../../components/layout';
import { postData, PostModel } from '../../sources/post-source';

export default ({ data }: { data: PostModel }) => {
    return (
        <Layout>
            <div>Post details</div>
            <div>{data.name}</div>
        </Layout>
    )
}

export async function getStaticProps({ params }) {
    const [data] = postData.filter(p => p.id === params.id);

    return {
        props: {
            data
        }
    }
}

export async function getStaticPaths() {
    const paths = postData.map(post => {
        return {
            params: {
                id: post.id
            }
        }
    })

    return {
        paths,
        fallback: false
    }
}