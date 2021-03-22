import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { useTranslation } from 'react-i18next';
import { AuthorizeUserData } from './LoginService';
import { TextInput } from 'Components/TextInput';
import { SubmitButton } from 'Components/SubmitButton';
import { Card } from 'Components/Card';

export function LoginPage() {
    const { t } = useTranslation();
    const loginLabel = t('login.login_label');
    const passwordLabel = t('login.password_label');
    const submitButton = t('login.submit');

    return (
        <Formik initialValues={{ login: '', password: '' }} onSubmit={({ login, password }) => { AuthorizeUserData(login, password); }}>
            {
                ({ values, handleChange }) => (
                    <Form>
                        <div className="w-full flex justify-center">
                            <div className="w-full lg:w-1/3 md:w-3/4 m-2">
                                <Card>
                                    <TextInput label={loginLabel} name="login" value={values.login} onChange={handleChange('login')} />
                                    <TextInput label={passwordLabel} name="password" value={values.password} onChange={handleChange('password')} />
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