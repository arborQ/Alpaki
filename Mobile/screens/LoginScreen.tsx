import * as React from 'react';
import { StyleSheet } from 'react-native';

import { Formik, Form } from 'formik';
import { Button, TextInput, Card, Title, Paragraph } from 'react-native-paper';

export function LoginScreen() {
  const [loginState, updateLoginState] = React.useState({ name: 'd' })
  return (
    <Formik initialValues={{ login: '', password: '' }} onSubmit={() => { }}>
      {
        ({ values, handleChange, submitForm }) => (
          <Card>
            <Card.Title title="Zaloguj się" subtitle="Podaj email i hasło" />
            <Card.Content>
              <TextInput
                label="Email"
                autoCompleteType="username"
                value={values.login}
                onChangeText={handleChange('login')}
              />
              <TextInput
                label="Hasło"
                autoCompleteType="password"
                value={values.password}
                onChangeText={handleChange('password')}
              />
            </Card.Content>
            <Card.Actions>
              <Button style={{ width: '100%' }} mode="contained" onPress={submitForm}>Zaloguj</Button>
            </Card.Actions>
            <Card.Actions>
              <Button style={{ width: '100%' }}>Nie pamiętasz hasła?</Button>
            </Card.Actions>
            <Card.Actions>
              <Button style={{ width: '100%' }} mode="outlined">Zarejestruj się</Button>
            </Card.Actions>
          </Card>
        )
      }
    </Formik>
  );
}

