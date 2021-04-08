import { useContext } from 'react';
import { Formik, Form } from 'formik';
import { useTranslation } from 'react-i18next';
import { AuthorizeUserData } from './LoginService';
import { TextInput } from 'Components/TextInput';
import { SubmitButton } from 'Components/SubmitButton';
import { Card } from 'Components/Card';
import { useHistory } from 'react-router';
import { AuthorizeContext } from 'Contexts/AuthorizeContext';

export function LoginPage() {
    const { t } = useTranslation();
    let history = useHistory();
    const authorizeContext = useContext(AuthorizeContext);

    const loginLabel = t('login.login_label');
    const passwordLabel = t('login.password_label');
    const submitButton = t('login.submit');

    return (
        <Formik initialValues={{ login: '', password: '' }} onSubmit={({ login, password }) => {
            AuthorizeUserData(login, password).then(user => {
                if (user) {
                    authorizeContext.updateUser(user);
                    history.push('/');
                }
            });
        }}>
            {
                ({ values, handleChange }) => (
                    <Form>
                        <div className="w-full flex justify-center">
                            <div className="w-full lg:w-1/3 md:w-3/4 m-2">
                                <Card>
                                    <TextInput label={loginLabel} name="login" value={values.login} onChange={handleChange('login')} />
                                    <TextInput label={passwordLabel} name="password" value={values.password} onChange={handleChange('password')} type="password" />
                                    <SubmitButton>{submitButton}</SubmitButton>
                                </Card>
                            </div>
                        </div>
                    </Form>
                )
            }
        </Formik>
    );
}