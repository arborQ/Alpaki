import { useContext } from 'react';
import { Formik, Form } from 'formik';
import { useTranslation } from 'react-i18next';
import { AuthorizeUserData } from './LoginService';
import { TextInput } from 'Components/TextInput';
import { SubmitButton } from 'Components/SubmitButton';
import { Card } from 'Components/Card';
import { useHistory } from 'react-router';
import { AuthorizeContext } from 'Contexts/AuthorizeContext';

import * as Yup from 'yup';

export function LoginPage() {
    const { t } = useTranslation();
    let history = useHistory();
    const authorizeContext = useContext(AuthorizeContext);

    const loginLabel = t('login.login_label');
    const passwordLabel = t('login.password_label');
    const submitButton = t('login.submit');

    const LoginDataValidation = Yup.object().shape({
        login: Yup.string().required(t('validation.loginRequired')),
        password: Yup.string().required(t('validation.passwordRequired')),
    });

    return (
        <Formik validationSchema={LoginDataValidation} initialValues={{ login: '', password: '' }} onSubmit={({ login, password }) => {
            AuthorizeUserData(login, password).then(user => {
                if (user) {
                    authorizeContext.updateUser(user);
                    history.push('/');
                }
            });
        }}>
            {
                ({ values, handleChange, errors }) => (
                    <Form autoComplete={'off'}>
                        <div className="w-full flex justify-center">
                            <div className="w-full lg:w-1/3 md:w-3/4 m-2">
                                <Card>
                                    <TextInput autoComplete={'off'} list="users" error={errors.login} label={loginLabel} name="login" value={values.login} onChange={handleChange('login')} />
                                    <TextInput error={errors.password} label={passwordLabel} name="password" value={values.password} onChange={handleChange('password')} type="password" />
                                    <SubmitButton>{submitButton}</SubmitButton>
                                </Card>
                            </div>
                            <datalist id="users">
                                <option value="admin" />
                                <option value="ola" />
                                <option value="kasia" />
                            </datalist>
                        </div>
                    </Form>
                )
            }
        </Formik>
    );
}