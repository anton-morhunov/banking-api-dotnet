import styles from "./Input.module.css";

function Input({children, ...props }) {
    
    return (
        <input className={`${styles.searchInput}`}
            {...props}
        />
    );
}

export default Input;