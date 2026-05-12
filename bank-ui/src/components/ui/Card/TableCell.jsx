import styles from "./Table.module.css";

function TableCell({ children }) {
    
    return(
        <td className={styles.td}>
            {children}
        </td>
    )
}

export default TableCell;