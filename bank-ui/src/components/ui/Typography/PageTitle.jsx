import styles from "./Typography.module.css";

function PageTitle({ children }) {

    return (
        <h1 className={styles.pageTitle}>
            {children}
        </h1>
    );
}

export default PageTitle;