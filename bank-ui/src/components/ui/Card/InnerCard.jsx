import styles from "./Table.module.css";

function InnerCard({ children }) {

    return(
        <div className={`${styles.innerCard} ${styles.innerCardRowStyle} ${styles.innerCardHeaderStyle}`}>
            {children}
        </div>
    )
}

export default InnerCard;