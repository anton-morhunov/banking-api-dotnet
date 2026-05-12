import styles from "./Button.module.css"

function BlockButton({children}) {
    return (
        <button
        className={`${styles.button} ${styles.dangerBtn}`}
        >
            {children}
        </button>
    );
}

export default BlockButton;