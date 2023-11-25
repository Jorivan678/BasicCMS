import { adminCommonStyles } from "../../generalUtils";

export function SetErrorStyles() {
    const styles = adminCommonStyles.filter((style) => style);
    styles.push({ key: 'error', value: '/assets/admin/css/pages/error.css' });
    styles.forEach(style => {
        const linkElement = document.createElement('link');
        linkElement.rel = 'stylesheet';
        linkElement.href = style.value;
        document.head.appendChild(linkElement);
    });
}