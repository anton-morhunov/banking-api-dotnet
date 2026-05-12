import styles from "./Button.module.css";

function PrimaryButton({children, ...props}) {
    return (
        <button
            className={`${styles.button} ${styles.primaryBtn}`}
            {...props}
        >
            {children}
        </button>
    );
}

export default PrimaryButton;