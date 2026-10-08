import styles from "./Header.module.css";

import wordflowLogo from "../assets/images/Logo.png";
import Account from "../components/Account/Account";

const Header = () => {
	return (
		<header className={styles.navbar}>
			<div className={styles.header}>
				<div className={styles.logo}>
					<img className={styles.logoImage} src={wordflowLogo} alt="Wordflow" />
				</div>

				<div className={styles.headerRight}>
					<Account />
				</div>
			</div>

			<hr className={styles.hr} />
		</header>
	);
};

export default Header;
