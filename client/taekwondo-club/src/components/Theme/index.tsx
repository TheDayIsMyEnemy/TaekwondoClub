import React from 'react'
import { useMantineColorScheme, useMantineTheme } from '@mantine/styles'
import { SunIcon, MoonIcon } from '@modulz/radix-icons'
import { Group, ActionIcon, Text } from '@mantine/core'
import { BookmarkFillIcon, BookmarkIcon } from '@primer/octicons-react'

export function Brand() {
  const theme = useMantineTheme()
  const { colorScheme, toggleColorScheme } = useMantineColorScheme()

  return (
    <div
      style={{
        paddingLeft: theme.spacing.xs,
        paddingRight: theme.spacing.xs,
        paddingBottom: theme.spacing.lg,
        borderBottom: `1px solid ${
          theme.colorScheme === 'dark'
            ? theme.colors.dark[4]
            : theme.colors.gray[2]
        }`,
      }}
    >
      <Group position="apart">
        <ActionIcon variant="light" size={30} color="orange">
          {colorScheme === 'dark' ? <BookmarkIcon /> : <BookmarkFillIcon />}
        </ActionIcon>
        <Text component="h4">Marky</Text>
        <ActionIcon
          variant="default"
          onClick={() => toggleColorScheme()}
          size={30}
        >
          {colorScheme === 'dark' ? <SunIcon /> : <MoonIcon />}
        </ActionIcon>
      </Group>
    </div>
  )
}
