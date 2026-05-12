import styles from "./Table.module.css";

function TableColumn({ children }) {

    return(
        <th className={styles.th}>
            {children}
        </th>
    )
}

export default TableColumn;