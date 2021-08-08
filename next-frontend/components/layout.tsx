export default function Layout({ children }) {
    return (
        <>
            <head>
                Header
            </head>
            <main>{children}</main>
            <footer>
                footer
            </footer>
        </>
    )
}