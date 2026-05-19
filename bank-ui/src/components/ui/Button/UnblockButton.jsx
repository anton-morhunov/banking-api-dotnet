import styles from './Button.module.css';
function UnblockButton({ children, onClick }) {
    
    return(
        <button 
            className={`${styles.button} ${styles.successBtn}`} 
            onClick={onClick}
        >
            {children}
        </button>
    );
}
export default UnblockButton;