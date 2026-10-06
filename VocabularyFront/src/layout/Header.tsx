import styles from "./Header.module.css";
import wordflowLogo from "../assets/images/Logo.png";
import Account from "../components/Account/Account";
import StatisticsMenu from "../components/StatisticsMenu/StatisticsMenu";

const Header = () => {
	return (
		<section className={styles.navbar}>
			<div className={styles.header}>
				<div className={styles.logo}>
					<img className={styles.logoImage} src={wordflowLogo} alt="Wordflow" />
				</div>
				<Account />
				<StatisticsMenu />
			</div>
			<hr className={styles.hr} />
		</section>
	);
};

export default Header;
