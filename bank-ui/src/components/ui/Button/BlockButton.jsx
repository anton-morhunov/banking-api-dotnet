import styles from "./Button.module.css"

function BlockButton({children, ...props}) {
    return (
        <button
        className={`${styles.button} ${styles.dangerBtn}`} {...props}
        >
            {children}
        </button>
    );
}

export default BlockButton;