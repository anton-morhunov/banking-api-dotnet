import { useNavigate } from "react-router-dom";
import styles from "./Sidebar.module.css";
import { Wallet } from "lucide-react"
import { User } from "lucide-react";
import { Users } from "lucide-react";

function Sidebar() {

    const navigate = useNavigate();

    return (
        <div className={styles.sidebar}>

            <h3 className={styles.sidebarTitle}>
                TheNorthField
                Bank
            </h3>

            <div
                className={styles.sidebarItem}
                onClick={() => navigate("/accounts")}
            >
                <Wallet size={24}/>
                <span>Accounts</span>
            </div>

            <div
                className={styles.sidebarItem}
                onClick={() => navigate("/home")}
            >
                <Users size={24}/>
                <span>Clients</span>
            </div>

            <div
                className={styles.sidebarItem}
                onClick={() => navigate("/colleagues")}
            >
                <User size={24}/>
                <span>Colleagues</span>
            </div>

        </div>
    );
}

export default Sidebar;