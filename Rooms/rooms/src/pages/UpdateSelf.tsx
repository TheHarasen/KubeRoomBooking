import User from "../components/UserConfig";
import { update } from "../services/UserService";

const Register = () => {

  return (
    <User func={update} passwordHtml="newPassword" passwordTitle="New Password">
    </User>
  );
}

export default Register;