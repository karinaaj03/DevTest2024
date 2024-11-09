import styles from '../Styles/App.module.css'
import PollSection from "@/Components/PollSection";

export default function Home() {
  return (
    <div className={styles.container} >
      <PollSection/>
    </div>
  );
}
