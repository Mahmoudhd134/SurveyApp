import { useTestQuery } from "../../Feutures/Auth/authApi";

const Profile = () => {
    const result = useTestQuery()

    return (
        <section>
            <h1>{JSON.stringify(result, null, '\n')}</h1>
        </section>
    );
};

export default Profile;