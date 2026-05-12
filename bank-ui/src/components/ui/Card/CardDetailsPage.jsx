import styles from "./Table.module.css";

function CardDetailsPage({ children }) {

    return(
        <div className={`${styles.card} ${styles.cardDetailsPage}`}>
            {children}
        </div>
    )
}

export default CardDetailsPage;