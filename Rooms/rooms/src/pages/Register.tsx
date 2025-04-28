import UserConfig from "../components/UserConfig";
import { register } from "../services/UserService";

const Register = () => {

  return (
    <UserConfig func={register} passwordHtml="repeatPassword" passwordTitle="Repeat the password">
    </UserConfig>
  );
}

export default Register;