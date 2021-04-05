import { Button } from '@material-ui/core';
import React, { PropsWithChildren, ReactElement } from 'react';

type PrimaryButtonProps = {
    href?: string,
    disabled?: boolean,
    onClick?: VoidFunction
}

const PrimaryButton = (props: PropsWithChildren<PrimaryButtonProps>): ReactElement<typeof Button> => <Button variant="contained" color="primary" href={props.href || ''} disabled={props.disabled || false} onClick={props.onClick}>{props.children}</Button>;

export default PrimaryButton;
