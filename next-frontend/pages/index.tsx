import Head from 'next/head'
import Link from 'next/link'

export default function Home() {
  return (
    <div className="container">
      <Head>
        <title>Next test app</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main>
      <Link href="/authorize">
          <a>Authorize</a>
        </Link>
        <Link href="/posts">
          <a>Posts</a>
        </Link>
        Hallo from TS
      </main>

      <footer>
       
      </footer>

      <style jsx>{`
        
      `}</style>

      <style jsx global>{`
        html,
        body {
          padding: 0;
          margin: 0;
          font-family: -apple-system, BlinkMacSystemFont, Segoe UI, Roboto,
            Oxygen, Ubuntu, Cantarell, Fira Sans, Droid Sans, Helvetica Neue,
            sans-serif;
        }

        * {
          box-sizing: border-box;
        }
      `}</style>
    </div>
  )
}
