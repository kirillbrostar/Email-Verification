export const translations = {
    en: {
        emailVerification: 'Email Verification',
        emailPlaceholder: 'Enter your email',
        sendCode: 'Send Code',
        enterCode: 'Enter Verification Code',
        codeSentTo: 'Code sent to',
        codePlaceholder: 'Enter 6-digit code',
        verify: 'Verify',
        verificationSuccess: 'Verification Successful!',
        emailVerified: 'Email verified',
        sending: 'Sending...',
        verifying: 'Verifying...',
        resend: {
            button: 'Resend code',
            sending: 'Sending...',
            wait: 'Resend available in {seconds} seconds'
        }
    },
    ru: {
        emailVerification: 'Подтверждение email',
        emailPlaceholder: 'Введите ваш email',
        sendCode: 'Отправить код',
        enterCode: 'Введите код подтверждения',
        codeSentTo: 'Код отправлен на',
        codePlaceholder: 'Введите 6-значный код',
        verify: 'Подтвердить',
        verificationSuccess: 'Почта подтверждена!',
        emailVerified: 'Email подтверждён',
        sending: 'Отправка...',
        verifying: 'Проверка...',
        resend: {
            button: 'Отправить код повторно',
            sending: 'Отправка...',
            wait: 'Повторная отправка через {seconds} секунд'
        }
    }
};

export type Language = keyof typeof translations;
export const defaultLanguage: Language = 'ru';