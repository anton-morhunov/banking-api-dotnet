import { useNavigate } from "react-router-dom";
import styles from "./Sidebar.module.css";

function Sidebar() {

    const navigate = useNavigate();

    return (
        <div className={styles.sidebar}>

            <h3 className={styles.sidebarTitle}>
                Panel
            </h3>

            <div
                className={styles.sidebarItem}
                onClick={() => navigate("/accounts")}
            >
                Accounts
            </div>

            <div
                className={styles.sidebarItem}
                onClick={() => navigate("/home")}
            >
                Clients
            </div>

            <div
                className={styles.sidebarItem}
                onClick={() => navigate("/colleagues")}
            >
                Colleagues
            </div>

        </div>
    );
}

export default Sidebar;