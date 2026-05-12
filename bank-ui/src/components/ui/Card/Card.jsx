import styles from "./Table.module.css";

function TableCard({ children }) {

    return(
        <div className={styles.card}>
            {children}
        </div>
    )
}

export default TableCard;